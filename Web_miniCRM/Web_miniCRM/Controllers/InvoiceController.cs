using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using OfficeOpenXml;
using Web_miniCRM.Domain.Entity;
using Web_miniCRM.Service.Interfaces;

namespace Web_miniCRM.Controllers
{
	public class InvoiceController : Controller
	{
		public readonly IInvoiceService _invoiceServices;
		public readonly ICompanyService _companyeService;
		public readonly IProductService _productService;

		public InvoiceController(IInvoiceService invoiceServices, ICompanyService companyeService, IProductService productService)
		{
			_invoiceServices = invoiceServices;
			_companyeService = companyeService;
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetInvoiceInfo(int id)
		{
			var response = await _invoiceServices.GetInvoiceId(id);
			var products = await _productService.GetProducts();

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var viewModel = new
				{
					Invoice = response.Data,
					Products = products.Data
				};

				return View(viewModel);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> GetInvoices()
		{
			var response = await _invoiceServices.GetInvoices();

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpGet]
		public async Task<IActionResult> GetInvoiceId(int id)
		{
			var response = await _invoiceServices.GetInvoiceId(id);

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return RedirectToAction("Error");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteInvoice(int id)
		{
			var response = await _invoiceServices.DeleteInvoice(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetCompanies");
			}
			return RedirectToAction("Error");
		}

		[HttpPost]
		public async Task<IActionResult> UpsertItem(Invoice model, string? action)
		{
			if(action == "CreateExelInvoice")
			{
				await CreateExelInvoice(model.InvoiceId);
			}
			else
			{
				var company = await _companyeService.GetCompanyId(model.CompanyId);
				model.Company = company.Data;
				//model.Informations = new List<Information>();

				if (ModelState.IsValid)
				{
					if (model.InvoiceId == 0)
					{
						await _invoiceServices.Insert(model);
						TempData["SuccessMessage"] = "Счет успешно создан";
					}
					else
					{
						await _invoiceServices.UpdateInvoice(model.InvoiceId, model);
						TempData["SuccessMessage"] = "Счет успешно изменен";
					}
				}
				else
				{
					var errors = ModelState.Values.SelectMany(v => v.Errors);
					foreach (var error in errors)
					{
						Console.WriteLine(error);
					}
				}
			}

			return RedirectToAction("GetInvoiceInfo", "Invoice", new {id = model.InvoiceId});
		}

		[HttpGet]
		public async Task<IActionResult> CreateInvoice(int? id)
		{
			var companiesRepository = await _companyeService.GetCompanies();
			var productsRepository = await _productService.GetProducts();
			if (companiesRepository.StatusCode == Domain.Enum.StatusCode.OK && productsRepository.StatusCode == Domain.Enum.StatusCode.OK)
			{
				var companies = id.HasValue ? companiesRepository.Data.Where(i => i.ManagerId == id).ToList() : companiesRepository.Data;
				var products = productsRepository.Data;

				return View(new CreateInvoiceViewModel(companies, products, new Information(), null));
			}

			return RedirectToAction("GetInvoices");
		}
		[HttpGet]
		public async Task<IActionResult> CreateExelInvoice(int invoiceId)
		{
			var invoice = await _invoiceServices.GetInvoiceId(invoiceId);
			var model = invoice.Data;
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage())
			{
				decimal sum = 0;
				// Добавляем новый лист в Excel файл
				var worksheet = package.Workbook.Worksheets.Add("Test");

				// Добавляем заголовки в первую строку
				worksheet.Cells["A1"].Value = "ID";
				worksheet.Cells["B1"].Value = "Name";
				worksheet.Cells["C1"].Value = "Quantity";
				worksheet.Cells["D1"].Value = "Price";

				// Заполняем данными
				for (int i = 0; i < model.InvoiceItems.Count; i++)
				{
					worksheet.Cells[$"A{i + 2}"].Value = i + 1;
					worksheet.Cells[$"B{i + 2}"].Value = model.InvoiceItems[i].Product.Name;
					worksheet.Cells[$"C{i + 2}"].Value = model.InvoiceItems[i].Quantity;
					worksheet.Cells[$"D{i + 2}"].Value = model.InvoiceItems[i].Product.Price;
					sum += model.InvoiceItems[i].Product.Price;
				}
				worksheet.Cells[$"C{model.InvoiceItems.Count + 2}"].Value = "Итого:";
				worksheet.Cells[$"D{model.InvoiceItems.Count + 2}"].Value = $"{sum}руб";
				try
				{
					// Сохраняем Excel файл
					var filePath = @"C:\Users\User\Desktop\Новая папка\123file.xlsx";
					FileInfo fileInfo = new FileInfo(filePath);
					package.SaveAs(fileInfo);

					TempData["SuccessMessage"] = $"Счет успешно создан в Exel формате по адресу {filePath}";

					// Возвращение на страницу с id=2
					//return RedirectToAction("GetInvoicesInfo", new { id = model.InvoiceId });
				}
				catch (Exception ex)
				{
					TempData["SuccessMessage"] = $"Не удалось создать счет, ошибка {ex.Message}";
				}
				return RedirectToAction("GetInvoiceInfo", new { id = model.InvoiceId });
			}
		}

	}
}

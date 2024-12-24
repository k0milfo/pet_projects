using System;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
public class Program
{
	static void Main(string[] args)
	{
		var sim = new InputSimulator();
		double screenWidth = 1250; // Замените на ваше реальное разрешение экрана по ширине
		double screenHeight = 768; // Замените на ваше реальное разрешение экрана по высоте

		// Задаем целевые координаты в пикселях
		double targetX = 1066;
		double targetY = 400;

		// Преобразуем координаты в абсолютные значения
		double absoluteX = (targetX / screenWidth) * 65535.0;
		double absoluteY = (targetY / screenHeight) * 65535.0;
		bool isHKeyPressed1 = false;
		bool isHKeyPressed2 = false;
		// Перемещаем мышь
		sim.Mouse.MoveMouseTo(absoluteX, absoluteY);
		while (!isHKeyPressed1)
		{
			sim.Mouse.MoveMouseTo(absoluteX, absoluteY);

			if (sim.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.VK_H))
			{
				isHKeyPressed1 = true;
			}
			if (sim.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.VK_J))
			{
				while (!isHKeyPressed2)
				{
					if (sim.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.VK_K))
					{
						isHKeyPressed2 = true;
						sim.Mouse.LeftButtonDown();
					}
					sim.Mouse.LeftButtonClick();
					sim.Mouse.LeftButtonDown();
					Thread.Sleep(300);
				}
			}
		}
	}
}
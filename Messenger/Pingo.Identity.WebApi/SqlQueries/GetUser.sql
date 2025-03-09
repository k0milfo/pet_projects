SELECT i.id, i.registration_time, j.Email, j.HashPassword, j.Salt
FROM "User" i
JOIN "UserCredentials" j ON i.id = j.user_id
WHERE j.Email = @Email;

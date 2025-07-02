
# Timezone Date Normalization API

A simple ASP.NET Core Web API that fixes issues with dates being saved incorrectly due to silent UTC conversion. It normalizes incoming `DateTime`, `DateOnly`, and nullable dates using the user's time zone, sent in an `X-Timezone` header.

---

## ✅ What It Does

- Accepts a time zone via `X-Timezone` (e.g. `Africa/Johannesburg`)
- Converts date/time inputs to match the user's local time
- Supports:
  - `DateTime`
  - `DateOnly`
  - Nullable versions of both
- Works globally via an action filter

---

## 🚀 Run and Test Locally with Docker

1. Build the Docker image:

```
docker build -t local-dotnet-utc
```
2. Run the container on port 5000:
```
docker run -p 5000:5000 local-dotnet-utc
```

3. Open Swagger in your browser:
```
http://localhost:5000/swagger
```

4. To test APIs needing the X-Timezone header, use Postman or another API tool, because Swagger can’t add custom headers easily.

5. To stop the container, press Ctrl + C or run:
```
docker ps
docker stop <container_id>
```

## 💡 Why It Matters

ASP.NET APIs often treat dates as UTC unless told otherwise. This project ensures dates are saved and returned as the user expects — based on their actual time zone.

---

## 🧱 Built With

* ASP.NET Core 8
* Action filters
* Swagger (Swashbuckle)
* TimeZoneInfo

---

## 📄 License
MIT

---
Made by Troy Krause

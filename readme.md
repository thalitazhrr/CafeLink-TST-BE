# API Documentation for CafeLink 🎧✮☕

---

## Deskripsi

---

CaféLink adalah platform inovatif yang menghubungkan pecinta kopi dengan kafe terbaik berdasarkan preferensi seperti lokasi, harga, jenis kopi, dan suasana. Dengan fitur pencarian canggih, personalisasi, dan halaman detail kafe yang mencakup menu, ulasan, serta deskripsi suasana, CaféLink mempermudah pengguna dalam menemukan dan menyimpan kafe favorit mereka. Menggabungkan teknologi modern dan antarmuka intuitif, CaféLink menciptakan pengalaman eksplorasi kopi yang cepat, relevan, dan menarik.

## Links

---

- 🔗[Website CafeLink](https://cafelink.vercel.app/)
- 🔗[API Documentation Swagger CafeLink](https://cafelinkapi.fly.dev/swagger/index.html)
- 🔗[Dokumen Laporan](https://docs.google.com/document/d/13BVkPa08oXTuf2T45C-E4PyelhkstBKWkhu_pnfjGnk/edit?usp=sharing)
- 🔗[Repository Frontend](https://github.com/thalitazhrr/CafeLink-TST)
- Integrasi dengan Teman: 🔗[API Documentation Swagger BucketQuest yang Saya Gunakan](https://bucketquestapi.fly.dev/swagger/index.html)

## Endpoints

### 1. User Registration
- **Method**: POST
- **Path**: /api/Account/register
- **Description**: Mendaftarkan pengguna baru.

### 2. User Login
- **Method**: POST
- **Path**: /api/Account/login
- **Description**: Mengautentikasi pengguna dan mengembalikan token akses.

### 3. Single Sign-On (SSO)
- **Method**: POST
- **Path**: /api/Account/Sso
- **Description**: Mengautentikasi pengguna melalui SSO.

### 4. Get Cafe Types
- **Method**: GET
- **Path**: /api/Cafe/cafe-types
- **Description**: Mengambil semua jenis kafe yang tersedia.

### 5. Get Coffee Types
- **Method**: GET
- **Path**: /api/Cafe/coffee-types
- **Description**: Mengambil semua jenis kopi yang tersedia.

### 6. Get All Cafes
- **Method**: GET
- **Path**: /api/Cafe/all
- **Description**: Mengambil semua data kafe yang tersedia.

### 7. Get Cafes
- **Method**: GET
- **Path**: /api/Cafe
- **Description**: Mengambil daftar kafe.

### 8. Get Cafe by ID
- **Method**: GET
- **Path**: /api/Cafe/{id}
- **Description**: Mengambil detail kafe berdasarkan ID.

### 9. Get Unique Coffees
- **Method**: GET
- **Path**: /api/Cafe/coffee/uniques
- **Description**: Mengambil daftar kopi unik.

### 10. Get Favorites
- **Method**: GET
- **Path**: /api/Favorites
- **Description**: Mengambil daftar kafe yang disukai pengguna.

### 11. Check Favorite Status
- **Method**: GET
- **Path**: /api/Favorites/check/{cafeId}
- **Description**: Memeriksa apakah kafe tertentu ada di daftar favorit pengguna.

### 12. Add Cafe to Favorites
- **Method**: POST
- **Path**: /api/Favorites/{cafeId}
- **Description**: Menambahkan kafe ke daftar favorit pengguna.

### 13. Remove Cafe from Favorites
- **Method**: DELETE
- **Path**: /api/Favorites/{cafeId}
- **Description**: Menghapus kafe dari daftar favorit pengguna. 

## Headers

| Header Name     | Required | Description                      |
|------------------|----------|----------------------------------|
| Content-Type     | Yes      | application/json                 |
| Authorization    | Yes      | Bearer {token}                  |

## Authentication
API ini menggunakan token Bearer untuk autentikasi. Pastikan Anda memiliki token akses yang valid sebelum melakukan request.

## Error Handling 
API ini akan mengembalikan kode kesalahan berikut jika terjadi masalah:
* `400 Bad Request`: Permintaan tidak valid.
* `401 Unauthorized`: Token akses tidak valid atau tidak ada.
* `404 Not Found`: Endpoint tidak ditemukan.
* `500 Internal Server Error`: Terjadi kesalahan di server.

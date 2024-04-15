# Ngay-Xua-Cu-MusicWeb

Project Ngay-Xua-Cu là Music Web ASP.Net core
Đầu tiên sau khi clone:
- Xoá Folder Migartion
- Project này có 2 DbContext sử dụng (Add-Migration "nameuwant" -context ApplicationDbContext (sử dụng câu lệnh tương tự cho AuthDbContext) và update-datase -context Application(AuthDbContext)DbContext). Remember to change DefaultConnection in appsettings.json

Khỏi chạy chương trình sẽ tự tạo một tài khoản admin, bạn có thể xem nó ở trong Program.cs, với mỗi tài khoản được tạo tiếp theo sẽ có role Listener

Chương trình có bug về phần CRUD Genre hiện tại nó không Create hoặc Edit

bạn có thể truy cập vào trang Admin bằng cách thay đổi Url /Admin

using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using FlowerShop.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using FlowerShop.Service;
using FlowerShop.Service.ServiceImpl;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//add mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add DbContext
builder.Services.AddDbContext<FlowerShopContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnectString")));

// Đăng ký DbFactory và UnitOfWork
builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Đăng ký Repository
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ICartDetailRepository, CartDetailRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IFeedBackResponseRepository, FeedBackResponseRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IPackagingRepository, PackagingRepository>();
builder.Services.AddScoped<IParameterConfigurationRepository, ParameterConfigurationRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductProductItemRepository, ProductProductItemRepository>();
builder.Services.AddScoped<ISaleInvoiceRepository, SaleInvoiceRepository>();
builder.Services.AddScoped<ISaleInvoiceDetailRepository, SaleInvoiceDetailRepository>();
builder.Services.AddScoped<ISupplierInvoiceDetailRepository, SupplierInvoiceDetailRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierInvoiceRepository, SupplierInvoiceRepository>();
builder.Services.AddScoped<IPaymentTokenRepository, PaymentTokenRepository>();

//đăng ký Service
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IProductItemService, ProductItemService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductProductItemService, ProductProductItemService>();
builder.Services.AddScoped<ISuppliersService, SuppliersService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISaleInvoiceService, SaleInvoiceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPackagingService, PackagingService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISupplierInvoiceService, SupplierInvoiceService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IParameterService, ParameterService>();
builder.Services.AddScoped<IFeedBackService, FeedBackService>();
builder.Services.AddScoped<ISaleInvoiceDetailService, SaleInvoiceDetailsService>();

//đăng ký Identity
//builder.Services.AddIdentity<AppUser, IdentityRole>()
//    .AddEntityFrameworkStores<FlowerShopContext>()
//    .AddDefaultTokenProviders();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = false; // Không yêu cầu số
	options.Password.RequiredLength = 1; // Đặt độ dài tối thiểu là 1
	options.Password.RequireNonAlphanumeric = false; // Không yêu cầu ký tự đặc biệt
	options.Password.RequireLowercase = false; // Không yêu cầu ký tự thường
	options.Password.RequireUppercase = false; // Không yêu cầu ký tự in hoa
})
.AddEntityFrameworkStores<FlowerShopContext>()
.AddDefaultTokenProviders();
// Cấu hình cookie cho Identity
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/login";
	options.LogoutPath = "/logout";
	options.AccessDeniedPath = "/access-denied";

	options.Events = new CookieAuthenticationEvents
	{
		OnRedirectToLogin = context =>
		{
			// Kiểm tra nếu là API request
			if (context.Request.Path.StartsWithSegments("/api") ||
				context.Request.Headers["Accept"].ToString().Contains("application/json"))
			{
				// Trả về 401 cho API
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				return Task.CompletedTask;
			}

			// Redirect về login cho MVC controller thông thường
			context.Response.Redirect(context.RedirectUri);
			return Task.CompletedTask;
		}
	};
});

//đăng ký session
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Không làm gì cả trong 30p thì session sẽ hết hạn
	options.Cookie.HttpOnly = true; // Cookie chỉ truy cập qua HTTP, không qua JavaScript(không cho phép js, các thư viện js đọc cookie này )
	options.Cookie.IsEssential = true; // Cookie này là thiết yếu, luôn được lưu bất kể quyền riêng tư
	options.Cookie.Name = "MyWeb";//đặt tên cookie lưu trữ id session trên trình duyệt của khách hàng
});

builder.Services.AddHttpContextAccessor(); // Đăng ký IHttpContextAccessor để hỗ trợ lấy base url

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
	// Đăng ký cho API Controllers
	endpoints.MapControllers();

	// Đăng ký cho area CHATBOT
	endpoints.MapAreaControllerRoute(
		name: "chatbot_area",
		areaName: "CHATBOT",
		pattern: "CHATBOT/{controller=Home}/{action=Index}/{id?}"
	);

	// Đăng ký cho area CUSTOMER
	endpoints.MapAreaControllerRoute(
		name: "customer_area",
		areaName: "CUSTOMER",
		pattern: "CUSTOMER/{controller=Home}/{action=Index}/{id?}"
	);

	// Đăng ký cho area ADMIN
	endpoints.MapAreaControllerRoute(
		name: "admin_area",
		areaName: "ADMIN",
		pattern: "ADMIN/{controller=Home}/{action=Index}/{id?}"
	);

	// Route mặc định
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}"
	);
});

// Seed dữ liệu mẫu
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await SeedData(services);
}

app.Run();
static async Task SeedData(IServiceProvider serviceProvider)
{
	// Lấy các dịch vụ cần thiết từ serviceProvider
	var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var dbContext = serviceProvider.GetRequiredService<FlowerShopContext>();

	// 1. Tạo các role (vai trò) nếu chưa tồn tại
	string[] roles = { "Admin", "Staff", "Customer" }; // Danh sách vai trò cần thêm
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}

	// 2. Kiểm tra và tạo người dùng "Admin"
	if (userManager.Users.All(u => u.UserName != "admin"))
	{
		// Tạo Cart trước
		var adminCart = new Cart();
		dbContext.Carts.Add(adminCart); // Thêm giỏ hàng vào DbContext
		await dbContext.SaveChangesAsync(); // Lưu để lấy Id

		// Tạo AppUser và gán CartId
		var adminUser = new AppUser
		{
			UserName = "admin", // Tên người dùng
			Email = "dungtienthe1920@gmail.com", // Email người dùng
			EmailConfirmed = true, // Xác nhận email
			PhoneNumberConfirmed = true, // Xác nhận số điện thoại
			FullName = "Dung Ne", // Họ và tên
			IsLock = false, // Trạng thái khóa
			IsDelete = false, // Trạng thái xóa
			BirthDay = DateTime.Parse("2003-04-30"), // Ngày sinh
			CartId = adminCart.Id, // Gán CartId từ giỏ hàng vừa tạo
			AccessFailedCount = 0, // Số lần đăng nhập thất bại
			LockoutEnabled = false, // Khóa tài khoản khi đăng nhập sai nhiều lần
		};

		// Tạo người dùng với mật khẩu
		var adminResult = await userManager.CreateAsync(adminUser, "123456"); // Mật khẩu là "123456"

		if (adminResult.Succeeded)
		{
			// 3. Gán vai trò "Admin" cho người dùng vừa tạo
			await userManager.AddToRoleAsync(adminUser, "Admin");
		}
		else
		{
			// Xử lý lỗi (nếu có)
			foreach (var error in adminResult.Errors)
			{
				Console.WriteLine($"Error: {error.Description}");
			}
		}
	}

	// 3. Kiểm tra và tạo người dùng "Customer"
	if (userManager.Users.All(u => u.UserName != "customer"))
	{
		// Tạo Cart trước
		var customerCart = new Cart();
		dbContext.Carts.Add(customerCart); // Thêm giỏ hàng vào DbContext
		await dbContext.SaveChangesAsync(); // Lưu để lấy Id

		// Tạo AppUser và gán CartId
		var customerUser = new AppUser
		{
			UserName = "customer", // Tên người dùng
			Email = "customer@example.com", // Email người dùng
			EmailConfirmed = true, // Xác nhận email
			PhoneNumberConfirmed = true, // Xác nhận số điện thoại
			FullName = "Nguyen Van Customer", // Họ và tên
			IsLock = false, // Trạng thái khóa
			IsDelete = false, // Trạng thái xóa
			BirthDay = DateTime.Parse("1995-01-01"), // Ngày sinh
			CartId = customerCart.Id, // Gán CartId từ giỏ hàng vừa tạo
			AccessFailedCount = 0, // Số lần đăng nhập thất bại
			LockoutEnabled = false, // Khóa tài khoản khi đăng nhập sai nhiều lần
		};

		// Tạo người dùng với mật khẩu
		var customerResult = await userManager.CreateAsync(customerUser, "customer123"); // Mật khẩu là "customer123"

		if (customerResult.Succeeded)
		{
			// 3. Gán vai trò "Customer" cho người dùng vừa tạo
			await userManager.AddToRoleAsync(customerUser, "Customer");
		}
		else
		{
			// Xử lý lỗi (nếu có)
			foreach (var error in customerResult.Errors)
			{
				Console.WriteLine($"Error: {error.Description}");
			}
		}
	}

	// Lưu thay đổi vào DbContext
	await dbContext.SaveChangesAsync();
}
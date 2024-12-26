document
  .querySelectorAll(".dropdown-submenu > .nav-link")
  .forEach(function (element) {
    element.addEventListener("click", function (e) {
      var parent = this.parentElement;
      parent.classList.toggle("open");
      e.stopPropagation();
      e.preventDefault();
    });
  });

//hàm đến trang xem chi tiết 1 sản phẩm
const DetailProduct = (id) => {
  fetchGet(
    "/api/product/detail?id=" + id,
    () => {
      //khum cần làm gì
    },
    () => {
      showError("Có lỗi xảy ra!");
    },
    () => {
      showError("Có lỗi xảy ra!");
    }
  );
};

const convertToVietnameseDong = (amount) => {
  const formatter = new Intl.NumberFormat("vi-VN", {
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  });

  return `${formatter.format(amount)} đ`;
};

const convertVietnameseDongToPrice = (moneyString) => {
  // Loại bỏ ký tự không phải số, như dấu phẩy, khoảng trắng và "đ"
  const numericString = moneyString.replace(/[^\d]/g, "");
  return parseInt(numericString, 10); // Chuyển đổi chuỗi số thành số nguyên
};

const isValidPhoneNumber = (phoneNumber) => {
  // Biểu thức chính quy để kiểm tra số điện thoại
  const phoneRegex = /^[+]?[0-9]{10,15}$/;

  // Kiểm tra chuỗi khớp với biểu thức chính quy
  return phoneRegex.test(phoneNumber);
};

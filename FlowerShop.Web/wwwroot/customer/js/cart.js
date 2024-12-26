//thêm 1 sản phẩm vào giỏ hàng
const AddProductToCart = (productId, quantity) => {
  // Ngăn chặn sự kiện click lan tới các phần tử cha
  event.stopPropagation();
  const uri = `/api/cart/add?id=${productId}&quantity=${quantity}`;
  fetchGet(
    uri,
    (success) => {
      if (success.message === "new") {
        const cartCountSpan = document.getElementById("count-cartdetail");
        let currentCount = parseInt(cartCountSpan.textContent) || 0;
        currentCount++;
        cartCountSpan.textContent = currentCount;
        showSuccess("Đã thêm sản phẩm mới vào giỏ hàng");
      } else {
        showSuccess("Đã thêm số lượng");
      }
    },
    (fail) => {
      showError(fail.message);
    },
    () => {
      showError("có lỗi xảy ra");
    }
  );
};

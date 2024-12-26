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
      alert(fail.message);
    },
    () => {
      alert("có lỗi xảy ra");
    }
  );
};


// Hàm cập nhật giao diện giỏ hàng
function UpdateCartUI() {
    console.log('Updating cart UI...');
    const uri = '/api/cart/count'; // Lấy tổng số lượng sản phẩm trong giỏ hàng
    fetch(uri, { method: 'GET' })
        .then(response => response.json())
        .then(data => {
            if (data.count !== undefined) {
                const cartCountSpan = document.getElementById("count-cartdetail");
                console.log("cartCountSpan:", cartCountSpan);  // Log giá trị của cartCountSpan để kiểm tra
                if (cartCountSpan) {
                    cartCountSpan.textContent = data.count; // Cập nhật số lượng
                    console.log("Updated cart count:", data.count);  // Log số lượng giỏ hàng đã cập nhật
                } else {
                    console.log("Element with id 'count-cartdetail' not found.");
                }
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showError("Có lỗi xảy ra khi cập nhật giỏ hàng");
        });
}

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

//thêm 1 sản phẩm vào giỏ hàng
const AddProductToCart = (productId, quantity) => {
  const uri = `/cart/add?id=${productId}&quantity=${quantity}`;
  fetchGet(
    uri,
    (success) => {
      if (success.message === "new") {
        const cartCountSpan = document.getElementById("count-cartdetail");
        let currentCount = parseInt(cartCountSpan.textContent) || 0;
        currentCount++;
        cartCountSpan.textContent = currentCount;
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

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


    fetchGet('/api/product/detail?id=' + id, () => {
        //khum cần làm gì
    }, () => {
        showError("Có lỗi xảy ra!");

    }, () => {
        showError("Có lỗi xảy ra!");
    })
}
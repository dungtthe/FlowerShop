function ShowOrderDetails(orderId) {
  if (!orderId) {
    showError("Không tìm thấy đơn hàng");
    return;
  }
  try {
    // Lấy thông tin từ row được chọn (tr) thông qua orderId
    const row = document.querySelector(`#hoadon-row-${orderId}`);
    const customerName = row.querySelector("td:nth-child(2)").innerText; // Tên khách hàng
    const createDate = row.querySelector("td:nth-child(3)").innerText; // Ngày tạo hóa đơn

    // Đặt thông tin khách hàng và ngày tạo hóa đơn vào modal
    document.getElementById("customerName").innerText = customerName;
    document.getElementById("createDate").innerText = createDate;

    fetchGet(
      `/api/admin/quan-li-don-hang/chi-tiet-don-hang/${orderId}`,
      (response) => {
        let tableBody = "";
        let total = 0;
        response.forEach((item) => {
          let thanhTien = item.unitPrice * item.quantity; // Thành tiền cho từng sản phẩm
          total += thanhTien; // Cộng dồn tổng tiền

          tableBody += `
                        <tr>
                            <td>${item.productId}</td>
                            <td>${item.product.title}</td>
                            <td>${item.quantity}</td>
                            <td>${item.unitPrice.toLocaleString("vi-VN", {
                              style: "currency",
                              currency: "VND",
                            })}</td>
                            <td>${thanhTien.toLocaleString("vi-VN", {
                              style: "currency",
                              currency: "VND",
                            })}</td>
                        </tr>
                    `;
        });

        // Lấy thông tin từ hóa đơn
        const saleInvoice = response[0]?.saleInvoice;
        const shippingCost = saleInvoice?.shippingCost || 0;
        const nameRecipient = saleInvoice?.nameRecipient || customerName;
        const isPaid = saleInvoice?.isPaid;
        const isPaidText = isPaid ? "Đã thanh toán" : "Chưa thanh toán";
        const phoneNumberRecipient =
          saleInvoice?.phoneNumberRecipient || "Không có thông tin";
        const deliveryAddress =
          saleInvoice?.deliveryAddress || "Không có thông tin";
        total += shippingCost;

        // Cập nhật nội dung bảng và tổng cộng
        document.querySelector("#orderDetailsTable tbody").innerHTML =
          tableBody;
        document.getElementById("orderTotal").innerText = total.toLocaleString(
          "vi-VN",
          { style: "currency", currency: "VND" }
        );
        document.getElementById("shippingFee").innerText =
          shippingCost.toLocaleString("vi-VN", {
            style: "currency",
            currency: "VND",
          });
        document.getElementById("recipientName").innerText = nameRecipient;
        document.getElementById("phoneNumber").innerText = phoneNumberRecipient;
        document.getElementById("address").innerText = deliveryAddress;
        document.getElementById("isPaid").innerText = isPaidText;

        // Hiển thị modal
        $("#orderDetailsModal").modal("show");
      },
      (res) => {
        showError(res.message);
      },
      () => {
        showError("Có lỗi khi kết nối đến server");
      }
    );

    // Đổi màu của dòng khi nhấn double-click
    const rowToHighlight = document.querySelector(`#hoadon-row-${orderId}`);
    if (rowToHighlight) {
      rowToHighlight.style.backgroundColor = "#f0f8ff"; // Màu nhạt báo hiệu
      setTimeout(() => {
        rowToHighlight.style.backgroundColor = ""; // Khôi phục màu ban đầu
      }, 2000);
    }
  } catch (error) {
    console.error(error);
    showError("Đã xảy ra lỗi khi lấy chi tiết đơn hàng.");
  }
}

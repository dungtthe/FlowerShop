//Hiển thị thông tin đơn nhập hàng
	function ShowOrderDetails(orderId) {
		if (!orderId) {
			showError("Không tìm thấy đơn hàng");
			return;
		}
		try {
			// Lấy thông tin từ row được chọn (tr) thông qua orderId
			const row = document.querySelector(`#hoadon-row-${orderId}`);
			const supplierName = row.querySelector('td:nth-child(2)').innerText; // Tên nhà cung vấp
			const createDate = row.querySelector('td:nth-child(3)').innerText;  // Ngày tạo đơn hàng
			const tongTien = row.querySelector('td:nth-child(4)').innerText;  // Tổng tiền

			// Đặt thông tin khách hàng và ngày tạo hóa đơn vào modal
			document.getElementById("supplierName").innerText = supplierName;
			document.getElementById("createDate").innerText = createDate;
			document.getElementById("orderTotal").innerText = tongTien;


			fetchGet(`/api/admin/quan-li-don-nhap/chi-tiet-don-nhap/${orderId}`,
				(response) => {
					console.log(response);
					let tableBody = "";
					response.forEach(item => {
						let thanhTien = item.unitPrice * item.quantity; // Thành tiền cho từng sản phẩm
						tableBody += `
																		<tr>
																			<td >${item.productItemId}</td>
																			<td>${item.productItem.name}</td>
																			<td>${item.quantity}</td>
																			<td>${item.unitPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
																			<td>${thanhTien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
																		</tr>
																		`;
					});

					// Cập nhật nội dung bảng và tổng cộng
					document.querySelector("#orderDetailsTable tbody").innerHTML = tableBody;
					$('#orderDetailsModal').modal('show');
				},
				(errorResponse) => {
					console.error(errorResponse);
					showError(errorResponse.message);
				},
				() => {
					showError("Không thể kết nối tới server");
				});

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

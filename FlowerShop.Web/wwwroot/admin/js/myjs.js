const convertToVietnameseDong = (amount) => {
  const formatter = new Intl.NumberFormat("vi-VN", {
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  });

  return `${formatter.format(amount)} đ`;
};

const ConvertVietnameseDongToNumber = (value) => {
  let result = "";
  for (const c of value) {
    // Kiểm tra nếu ký tự là số
    if (!isNaN(c) && c !== " ") {
      result += c;
    }
  }
  return parseInt(result);
};

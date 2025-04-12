$(document).ready(function () {
    ShowCount();

    // Thêm sản phẩm vào giỏ hàng
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var fragrance = $(this).data('fragrance');
        var quantity = $(this).closest('tr').find('.txtQuantity').val();
        Update(id, fragrance, quantity);
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        const fragrance = $(this).data('fragrance');
        console.log("Xoá:", { id, fragrance });
        Delete(id, fragrance);
    });

    // Xóa toàn bộ giỏ hàng
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        if (confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?')) {
            DeleteAll();
        }
    });

    // Thay đổi số lượng bằng input (tự động cập nhật)
    $('body').on('change', '.txtQuantity', function () {
        var id = $(this).data('id');
        var fragrance = $(this).data('fragrance');
        var quantity = $(this).val();
        Update(id, quantity, fragrance);
    });
});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}

function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function Update(id, fragrance, quantity) {
    quantity = parseInt(quantity);

    console.log("Gửi cập nhật:", { id, fragrance, quantity });

    $.ajax({
        url: "/shoppingcart/Update",
        type: "POST",
        data: { id: id, fragrance: fragrance, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                location.reload();
            } else {
                alert("Lỗi khi cập nhật: " + rs.msg);
            }
        }
    });
}


function Delete(id, fragrance) {
    var conf = confirm('Bạn có muốn xóa sản phẩm này không?');
    if (conf === true) {
        $.ajax({
            url: '/shoppingcart/Delete',
            type: 'POST',
            data: { id: id, fragrance: fragrance },
            success: function (rs) {
                if (rs.Success) {
                    location.reload();
                }
            }
        });
    }
}

// Tải lại giao diện giỏ hàng (danh sách sản phẩm và phần tóm tắt thanh toán)
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
    $.ajax({
        url: '/shoppingcart/Partial_Item_ThanhToan',
        type: 'GET',
        success: function (rs) {
            $('#cart_summary').html(rs);
        }
    });
}
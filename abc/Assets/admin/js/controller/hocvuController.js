var hocvu = {
    init: function () {
        hocvu.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/HocVu/ChangeTinhTrang",
                data: { id : id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Hoàn thành');
                       
                    }
                    else {
                        btn.text('Chưa hoàn thành');
                        
                    }
                }
            });
        });
    }
}
hocvu.init();
/**
    全局常量文件
**/
; (function ($) {

    function JxWebSite() {
        // 客户申请类型
        this.ClientApplyType = {
            // 客户预约
            ClientSubscribe: 1,
            // 产品申请
            ProductApply:2
        }
    }

    var jxWebSite = new JxWebSite();

    $.JxWebSite = Window.JxWebSite = jxWebSite;

})(jQuery);
;(function ($) {

    function Util() {
        // 验证器：不通过统一返回true
        this.validator = {
            specialChar : function(value){
                return (/[\~\!\#\>\<\?\/\$\%\^\&\*\(\)@@\！\￥\、\（\）]/gi.test(value));
            },
            empty: function (value) {
                return (value == null || value == undefined || value == '' || value.trim() == '');
            },
            phone: function (value) {
                //return !/^(13[0-9]|15[0-]|18[0-9])\d{8}$/.test(value);
                return !/^1\d{10}$/.test(value);
            }
        }
    }

    window.util = $.util = new Util();

})(jQuery);
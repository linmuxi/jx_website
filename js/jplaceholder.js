var JPlaceHolder = {
    //检测
    _check : function(){
        return 'placeholder' in document.createElement('input');
    },
    //初始化
    init : function(){
        if(!this._check()){
            this.fix();
        }
    },
    //修复
    fix : function(){
        jQuery(':input[placeholder]').each(function(index, element) {
            var self = $(this), txt = self.attr('placeholder');
            self.val(txt);
            self.focusin(function(e) {
                if(self.val() == txt){
                    self.val('');
                }
            }).focusout(function(e) {
                if(!self.val()){
                    self.val(txt);
                }
            });
            holder.click(function(e) {
                self.val('');
                self.focus();
            });
        });
    }
};
//执行
jQuery(function(){
    JPlaceHolder.init();    
});
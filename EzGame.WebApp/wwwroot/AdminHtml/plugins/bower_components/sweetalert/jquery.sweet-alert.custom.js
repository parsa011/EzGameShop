
!function($) {
    "use strict";

    var SweetAlert = function() {};

    //examples 
    SweetAlert.prototype.init = function() {
        
    //Basic
    $('#sa-basic').click(function(){
        swal("اینجا یک پیام است!");
    });

    //A title with a text under
    $('#sa-title').click(function(){
        swal("اینجا یک پیام است!", "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.")
    });

    //Success Message
    $('#sa-success').click(function(){
        swal("شغل خوب!", "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است.", "موفقیت")
    });

    //Warning Message
    $('#sa-warning').click(function(){
        swal({   
            title: "شما مطمئن هستید؟",   
            text: "شما نمیتوانید این فایل خیالی را بازیابی کنید!",   
            type: "هشدار",   
            showCancelButton: true,   
            confirmButtonColor: "#DD6B55",   
            confirmButtonText: "بله، آن را حذف کنید!",   
            closeOnConfirm: false 
        }, function(){   
            swal("حذف شده!", "فایل خیالی شما حذف شده است.", "موفقیت"); 
        });
    });

    //Parameter
    $('#sa-params').click(function(){
        swal({   
            title: "شما مطمئن هستید؟",   
            text: "شما نمیتوانید این فایل خیالی را بازیابی کنید!",   
            type: "هشدار",   
            showCancelButton: true,   
            confirmButtonColor: "#DD6B55",   
            confirmButtonText: "بله، آن را حذف کنید!",   
            cancelButtonText: "نه لغو شود لطفا!",   
            closeOnConfirm: false,   
            closeOnCancel: false 
        }, function(isConfirm){   
            if (isConfirm) {     
                swal("حذف شده!", "فایل خیالی شما حذف شده است.", "موفقیت");   
            } else {     
                swal("لغو شد", "فایل خیالی شما امن است :)", "خطا");   
            } 
        });
    });

    //Custom Image
    $('#sa-image').click(function(){
        swal({   
            title: "گاویندا!",   
            text: "به تازگی به عضویت تویتتر درآمد",   
            imageUrl: "../plugins/images/users/govinda.jpg" 
        });
    });

    //Auto Close Timer
    $('#sa-close').click(function(){
         swal({   
            title: "هشدار خودکار",   
            text: "من در عرض 2 ثانیه بسته خواهم شد",   
            timer: 2000,   
            showConfirmButton: false 
        });
    });


    },
    //init
    $.SweetAlert = new SweetAlert, $.SweetAlert.Constructor = SweetAlert
}(window.jQuery),

//initializing 
function($) {
    "use strict";
    $.SweetAlert.init()
}(window.jQuery);
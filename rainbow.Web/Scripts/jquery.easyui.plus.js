
//拓展easyui的Messager插件

/**
* 在iframe中调用，在父窗口中出提示框(herf方式不用调父窗口)
*/
$.extend({
    messageBox5s: function (title, msg) {
        $.messager.show({
            title: title,
            msg: msg,
            timeout: 5000,
            showType: 'slide',
            style: {
                left: '',
                right: 5,
                top: '',
                bottom: -document.body.scrollTop - document.documentElement.scrollTop + 5
            }
        });
    }
});

$.extend({
    messageBox10s: function (title, msg) {
        $.messager.show({
            title: title,
            msg: msg,
            height: "auto",
            width: "auto",
            timeout: 10000,
            showType: "slide",
            style: {
                left: '',
                right: 5,
                top: '',
                bottom: -document.body.scrollTop - document.documentElement.scrollTop + 5
            }
        });
    }
});


$.extend({
    show_alert: function (title, msg) {
        $.messager.alert(title, msg);
    }
});

/**
*panel关闭时回收内存，主要用于layout使用iframe嵌入网页时的内存泄漏问题
*/
$.fn.panel.defaults.onBeforeDestroy = function () {
    var frame = $('iframe', this);
    try {
        if (frame.length > 0) {
            for (var i = 0; i < frame.length; i++) {
                frame[i].contentWindow.document.write('');
                frame[i].contentWindow.close();
            }
            frame.remove();
            if ($.browser.msie) {
                CollectGarbage();
            }
        }
    } catch (e) {

    }
};

/**
*对easyui插件做一点拓展
*/

//easyui-window默认不居中显示
var easyuiOnOpen = function (left, top) {
    var iframeWidth = $(this).parent().parent().width();
    var iframeHeight = $(this).parent().parent().height();
    var windowWidth = $(this).parent().width();
    var windowHeight = $(this).parent().height();
    var setWidth = (iframeWidth - windowWidth) / 2;
    var setHeight = (iframeHeight - windowHeight) / 2;
    $(this).parent().css({
        left: setWidth,
        top: setHeight
    });
    if (iframeHeight < windowHeight) {
        $(this).parent().css({
            left: setWidth,
            top: 0
        });
    }
    $(".window-shadow").hide();
}
$.fn.window.defaults.onOpen = easyuiOnOpen;

//easyui-window点击最大化后缩小出现不居中问题

var easyuiPanelOnResize = function (left, top) {
    var iframeWidth = $(this).parent().parent().width();
    var iframeHeight = $(this).parent().parent().height();
    var windowWidth = $(this).parent().width();
    var windowHeight = $(this).parent().height();
    var setWidth = (iframeWidth - windowWidth) / 2;
    var setHeight = (iframeHeight - windowHeight) / 2;

    $(this).parent().css({
        left: setWidth - 6,
        top: setHeight - 6
    });
    if (iframeHeight < windowHeight) {
        $(this).parent().css({
            left: setWidth,
            top: 0
        });
    }
    $(".window-shadow").hide();
}
$.fn.window.defaults.onResize = easyuiPanelOnResize;

//自动化datagrid，从第一次加载与重置窗口大小时候发生的事件
$(function () {
    $(window).resize(function () {
        $("#List").datagrid('resize', {
            width: $(window).width() - 10,
            height: $(window).height() - 35
        }).datagrid('resize', {
            width: $(window).width() - 10,
            height: $(window).height() - 35
        });
    });
});

//datagrid的日期时间格式化方法
function dataFormatter(value) {
    var date = new Date(value);
    var y = date.getFullYear().toString();
    var M = date.getMonth() + 1;
    var d = date.getDate().toString();
    var h = date.getHours().toString();
    var m = date.getMinutes().toString();
    var s = date.getSeconds().toString();
    if (M < 10) {
        M = "0" + M;
    }
    if (d < 10) {
        d = "0" + d;
    }
    if (h < 10) {
        h = "0" + h;
    }
    if (m < 10) {
        m = "0" + m;
    }
    if (s < 10) {
        s = "0" + s;
    }
    //return y + "-" + M + "-" + d + " " + h + ":" + m + ":" + s;
    return y + "-" + M + "-" + d + " " + h + ":" + m
}


//datagrid分页显示
//$(function () {
//    $("#List").datagrid("getPager").pagination({
//        pageSize: 10,
//        pageNumber: 1,
//        pageList: [10, 20, 30, 40],
//        beforePageText: "第",
//        afterPageText: "页",
//        displayMsg: "显示{from}到{to}条记录 共{total}条记录"
//    });
//})

//没有按钮的操作权限提示
function NoRight() {
    $.show_alert("提示", "你没有该按钮的操作权限");
    return false;
}

//替换DataGrid中的width和height计算
function SetGridWidthSub(w) {
    return $(window).width() - w;
}
function SetGridHeightSub(h) {
    return $(window).height() - h
}
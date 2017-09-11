$(function () {
    $("#tab_menu_tabrefresh").click(function () {
        //重新设置标签
        var url = $(".tabs-panels .panel").eq($(".tabs-selected").index()).find("iframe").attr("src");
        $(".tabs-panels .panel").eq($(".tabs-selected").index()).find("iframe").attr("src", url);
    });
    //在新窗口打开该标签
    $("#tab_menu_openFrame").click(function () {
        var url = $(".tabs-panels .panel").eq($(".tabs-selected").index()).find("iframe").attr("src");
        window.open(url);
    });
    //关闭当前
    $("#tab_menu-tabclose").click(function () {
        var current_title = $(".tabs-selected .tabs-inner span").text();
        $("#mainTab").tabs("close", current_title);
        if ($(".tabs li").length == 0) {
            $(".layout-button-right").trigger("click");
        }
    });
    //全部关闭
    $("#tab_menu_tabcloseall").click(function () {
        $(".tabs-inner span").each(function (i, n) {
            if ($(this).parent().next().is(".tabs-close")) {
                var t = $(n).text();
                $("#mainTab").tabs("close", t);
            }
        });
        $(".layout-button-right").trigger("click");
    });
    //关闭除当前之外的所以窗口
    $("#tab_menu_tabcloseother").click(function () {
        var current_title = $(".tabs-selected .tabs-inner span").text();
        $(".tabs-inner span").each(function (i, n) {
            if ($(this).parent().next().is(".tabs-close")) {
                var t = $(n).text();
                if (t != current_title) {
                    $("#mainTab").tabs("close", t);
                }
            }
        });
    });
    //关闭当前右侧的窗口
    $("#tab_menu-tabcloseright").click(function () {
        var nextall = $(".tabs-selected").nextAll();
        if (nextall.length == 0) {
            $.messager.alert("提示", "前面没有了", "warning");
            return false;
        }
        nextall.each(function (i, n) {
            if ($("a.tabs-close", $(n)).length > 0) {
                var t = $("a:eq(0) span", $(n)).text();
                $("#mainTab").tabs("close", t);
            }
        });
        return false;
    });
    //关闭当前左边的窗口
    $("#tab_menu-tabcloseleft").click(function () {
        var prevall = $(".tabs-selected").prevAll();
        if (prevall.length == 0) {
            $.messager.alert("提示", "后面没有了", "warning");
            return false;
        }
        prevall.each(function (i, n) {
            if ($("a.tabs-close", $(n)).length > 0) {
                var t = $("a:eq(0) span", $(n)).text();
                $("#mainTab").tabs("close", t);
            }
        });
        return false;
    })
});

$(function () {
    //为选项卡绑定右键
    $(".tabs li").on("contextmenu", function (e) {
        var subtitle = $(this).text();
        $("#mainTab").tabs("select", subtitle);
        $("#tab_menu").menu("show", {
            left: e.pageX,
            top: e.pageY
        });
        return false;
    });
});

function addTab(subtitle, url, icon) {
    if (!$("#mainTab").tabs("exists", subtitle)) {
        $("#mainTab").tabs("add", {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $("#mainTab").tabs("select", subtitle);
        $("#tab_menu_tabrefresh").trigger("click");
    }
    $(".layout-button-left").trigger("click");//自动回收菜单列表
}

function createFrame(url) {
    var s = "<iframe frameborder='0' src='" + url + "' scrolling='auto' style='width:100%;height:99%;'></iframe>";
    return s;
}

//切换皮肤
$(function () {
    $(".ul-skin-nav .li-skinitem span").click(function () {
        var theme = $(this).attr("rel");
        $.messager.alert("提示", "切换皮肤将重新加载系统", function (e) {
            if (e) {
                $.post("../../Home/SetThemes", { value: theme }, function (data) {
                    window.location.reload();
                }, "json");
            }
        });
    });
});


//左侧导航树

$(function () {
    var o = {
        showcheck: false,
        url: "/Home/GetTree",
        onnodeclick: function (item) {
            var tabTitle = item.text;
            var url = "../../" + item.value;
            var icon = item.Icon;
            if (!item.hasChildren) {
                addTab(tabTitle, url, icon);
            } else {
                $(this).parent().find("img").trigger("click");
            }
        }
    }
    $.post("/Home/GetTree", { "id": "0" }, function (data) {
        if (data == "0") {
            window.location = "/Account";
        }
        o.data = data;
        $("#RightTree").treeview(o);
    }, "json");
});
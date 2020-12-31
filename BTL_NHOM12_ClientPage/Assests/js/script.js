jQuery(document).ready(function () {
    GoTopPage();
    Response.action(function () {
        if (Response.band(600)) {  //>= value
            $('#main-search').after($('#cart-box'));
        }
        if (Response.band(1000)) {  //>= value
            $('.hm-bn').before($('nav.menu'));
            $(".mobile-menu").removeClass("mopened");
            $("body").removeClass("bm-opened");
        }
        else {  //< 768
            $('#mobile-menu').html($('nav.menu'));
            $('#mobile-menu .has-cat-mega h4').on('click', function () {
                var tOld = $('.mobile-menu .has-cat-mega h4');
                if (tOld.hasClass('on')) {
                    tOld.removeClass('on');
                    tOld.parent().find('.cat-mega-menu').css({ 'visibility': 'hidden', 'height': 0, 'opacity': 0 });
                }

                var t = $(this);
                t.addClass('on');
                t.parent().find('.cat-mega-menu').slideDown().css({ 'visibility': 'visible', 'height': 'auto', 'opacity': 1 });
            });
        }
    });
    $(document).mouseup(function (e) {
        var container = $(".cart-sidebar");
        if (!container.is(e.target)
            && container.has(e.target).length === 0) {
            $('body').removeClass('cart-opened');
            $('.cart-sidebar').removeClass('opened');
        }
        var container2 = $(".mobile-menu");
        if (!container2.is(e.target)
            && container2.has(e.target).length === 0) {
            $('body').removeClass('bm-opened');
            $('.mobile-menu').removeClass('mopened');
        }
    });
    $('#mobileMenu').on('click', function () {
        $(".mobile-menu").addClass("mopened");
        $("body").addClass("bm-opened");
    });
    $("header").sticky({ topSpacing: 0, zIndex: 999 });
});

$('#sSrch').on('click', function (e) {
    $('#main-search').toggle();
});

function GoTopPage() {
    $("#gb-top-page").length && ($("#gb-top-page").hide(), $(window).scroll(function () {
        $(this).scrollTop() > 100 ? $("#gb-top-page").fadeIn() : $("#gb-top-page").fadeOut()
    }), $("#gb-top-page").click(function () {
        return $("body,html").animate({
            scrollTop: 0
        }, 800), !1
    }))
}

var openCart = false;
function showCart(id, qty) {
    var odrNum = parseInt($('#cbCount').text()) + 1;
    $('#cbCount').html(odrNum);
    $('#cb-cart-badge').html(odrNum);

    var obj = {};
    obj.idPro = id;
    obj.Qty = qty == -1 ? document.getElementById('txtQty').value : qty;
    var jsonData = JSON.stringify(obj);
    $.ajax({
        url: '/add-quick-order.submit?a=add',
        type: 'POST',
        data: jsonData,
        success: function (data) {
            $('#showItemOrder').html(data);
            $('#list-cart').html(data);
        }
    });

    openCart = true;
    jQuery(".cart-sidebar").addClass("opened");
    jQuery("body").addClass("cart-opened");
    return false;
}

function hideCart() {
    openCart = false;
    jQuery(".cart-sidebar").removeClass("opened");
    jQuery("body").removeClass("cart-opened");
    return false;
}
//Window Scroll
jQuery(window).scroll(function () {
    //Scroll Top
    var h = $(window).height();
    var w = $(window).width();
    var st = $(window).scrollTop();
    var condition1 = $(window).scrollTop() + $(window).height() > $(document).height() - 200;
    if (w > 800) {
        if (st > 360) {
            $('.mainmenu .line').after($('nav.menu'));
        } else {
            if ($('.hm-bn').length > 0) $('.hm-bn').before($('nav.menu'));
        }
    }
});

jQuery.loadScript = function (url, callback) {
    jQuery.ajax({
        url: url,
        dataType: 'script',
        success: callback,
        async: true
    });
}

$('#keySearch').on('click', function (e) {
    $.loadScript('/js/jquery-ui.min.js', function () {
        $(document).ready(function () {
            $("#keySearch").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/search.submit?tk=" + request.term,
                        type: 'POST',
                        success: function (data) {
                            response(data);
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    return false;
                }
            }).autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<a href='/" + item.Link + ".html'><img src='/img/" + item.Img + "' align='left'/><div><span>" + item.Name + "</span>" + item.PriceOff + "" + item.Price + "" + item.ProSaleOff + "</div></a>")
                    .appendTo(ul);
            };
        });

    });

});



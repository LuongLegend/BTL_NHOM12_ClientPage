﻿$(window).load(function () {
    var sync1 = $("#sync1");
    var sync2 = $("#sync2");

    sync1.owlCarousel({
        autoPlay: 5000,
        singleItem: true,
        slideSpeed: 500,
        navigation: true,
        pagination: false,
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
        lazyLoad: true,
        stopOnHover: true
    });

    sync2.owlCarousel({
        items: 5,
        itemsDesktop: [1199, 5],
        itemsDesktopSmall: [979, 4],
        itemsTablet: [768, 3],
        itemsMobile: [479, 2],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function (el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });

    function syncPosition(el) {
        var current = this.currentItem;
        $("#sync2")
            .find(".owl-item")
            .removeClass("synced")
            .eq(current)
            .addClass("synced")
        if ($("#sync2").data("owlCarousel") !== undefined) {
            center(current);
        }
    }

    $("#sync2").on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }

        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        } else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1])
        } else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1)
        }

    }
    //#endregion
    callSlider($('#promotion'), false, false, false);
    callSlider($('.shockbox'), true);
    callSlider($('#brand'), false, true, false);
    callSlider($('.homenewsf'), false);
});

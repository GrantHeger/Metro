
    $(document).ready(function() {
        $("div.info").hide();
        $("h2.expand, h3.expand").addClass("inactive");
        $("h2.expand, h3.expand").toggle(function() {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
                $(this).addClass("inactive");
            }
            else {
                $(this).addClass("active");
                $(this).removeClass("inactive");
            }
        }, function() {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
                $(this).addClass("inactive");
            }
            else {
                $(this).addClass("active");
                $(this).removeClass("inactive");
            }
        });
        $("h2.expand, h3.expand").click(function() {
            $(this).next("div.info").slideToggle("slow");
            //$(this).siblings("div.info").hide();
        });
    });

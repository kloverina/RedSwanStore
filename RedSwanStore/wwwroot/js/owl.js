

$(function() {
    // Owl Carousel
    var owl = $(".owl-carousel");
    owl.owlCarousel({
        items: 1,
        autoplay: true,
        autoplayHoverPause: true,
        autoplayTimeout: 3000,
        loop: true,
        lazyLoad:false,
        margin: 50,
        merge: false,
        center:true,
        nav: false,
        video: true,
        videoWidth: false,
        videoHeight: false
    });
    
    $('.owl-video-play-icon').on('click', function () {
        owl.trigger('stop.owl.autoplay');
    })
});



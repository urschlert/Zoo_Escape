$(document).ready(function(){
    // random background images
    var classCycle=['image1','image2','image3'];
    var randomNumber = Math.floor(Math.random() * classCycle.length);
    var classToAdd = classCycle[randomNumber];
    $('body').addClass(classToAdd);
});
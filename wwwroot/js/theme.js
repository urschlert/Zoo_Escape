window.PlayMainTheme = function() {
    document.getElementById('maintheme').pause();
    document.getElementById('maintheme').currentTime = 0;
    document.getElementById('maintheme').play();
};
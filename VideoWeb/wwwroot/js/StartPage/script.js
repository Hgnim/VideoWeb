function playVideo() {
    var objSelect = document.getElementById("sel");
    const videoUrl = objSelect.options[objSelect.selectedIndex].text;
    const videoElement = document.getElementById('videoPlayer');

    if (videoElement) {
        videoElement.src = videoUrl;
        videoElement.play();
    }
}
function pathSelect() {

}
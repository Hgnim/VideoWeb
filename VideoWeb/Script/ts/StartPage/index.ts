import '../../scss/StartPage/index.scss';
import { getCookie, setCookie } from '../global/cookie'
export function init() {
    (window as any).playVideo = playVideo;
    (window as any).cookieVolumeChange = cookieVolumeChange;
}
function playVideo() {
    const objSelect:HTMLSelectElement = document.getElementById("sel") as HTMLSelectElement;
    const videoUrl = objSelect.options[objSelect.selectedIndex].text;
    const videoElement:HTMLVideoElement = document.getElementById('videoPlayer') as HTMLVideoElement;

    if (videoElement) {
        videoElement.src = `${objSelect.dataset.urlroot}/${videoUrl}`;
        videoElement.play();
    }
}
function pathSelect() {

}

document.addEventListener('DOMContentLoaded', function () {
    {
        const vp: HTMLVideoElement = document.getElementById('videoPlayer') as HTMLVideoElement;
        vp.volume = parseFloat(getCookie('volume') || '0.8');
    }
});

function cookieVolumeChange() {
    setCookie('volume', (document.getElementById('videoPlayer') as HTMLVideoElement).volume.toString(), 365);
}
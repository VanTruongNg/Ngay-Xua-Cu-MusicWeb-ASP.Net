var audioPlayer = document.getElementById('audioPlayer');
var playPauseButton = document.getElementById('playPauseButton');
var playNextButton = document.getElementById('playNextButton');
var playPrevButton = document.getElementById('playPrevButton');
var loopButton = document.getElementById('loopButton');
var suffleButton = document.getElementById('suffleButton');
var songTitle = document.getElementById('songTitle');
var songArtist = document.getElementById('songArtist');
var songImage = document.getElementById('songImage');
var progressBar = document.getElementById('progressBar');
var currentTime = document.getElementById('currentTime');
var duration = document.getElementById('duration');
var progressContainer = document.querySelector('.progress-container');
var songCards = document.querySelectorAll('.song-details');

var currentIndex = 0;

function playSong(mp3FilePath, name, artist, imgFilePath) {
    // Cập nhật thông tin bài hát
    songTitle.innerText = name;
    songArtist.innerText = artist;
    songImage.src = imgFilePath;

    // Cập nhật source audio và phát nhạc
    audioPlayer.src = mp3FilePath;
    audioPlayer.play();
    playPauseButton.classList.remove('fa-play');
    playPauseButton.classList.add('fa-pause');

    currentIndex = songs.findIndex(song => song.mp3FilePath === mp3FilePath);
}

function onAudioEnded() {
    playNextSong();
}
audioPlayer.addEventListener('ended', onAudioEnded);


// Xử lý sự kiện play/pause
playPauseButton.addEventListener('click', function () {
    if (audioPlayer.paused) {
        audioPlayer.play();
        playPauseButton.classList.remove('fa-play');
        playPauseButton.classList.add('fa-pause');
    } else {
        audioPlayer.pause();
        playPauseButton.classList.remove('fa-pause');
        playPauseButton.classList.add('fa-play');
    }
})

// Hàm lấy thông tin bài hát và đẩy vào danh sách
var songs = [];
songCards.forEach(function (songCard) {
    var mp3FilePath = songCard.getAttribute('data-mp3');
    var name = songCard.getAttribute('data-name');
    var artist = songCard.getAttribute('data-artist');
    var imgFilePath = songCard.getAttribute('data-img');
    songs.push({ mp3FilePath: mp3FilePath, name: name, artist: artist, imgFilePath: imgFilePath });
});

// Hàm xử lý sự kiện next song
function playNextSong() {
    currentIndex++;
    if (currentIndex >= songs.length) {
        currentIndex = 0; // Quay lại đầu danh sách nếu đang ở cuối danh sách
    }
    playSong(songs[currentIndex].mp3FilePath, songs[currentIndex].name, songs[currentIndex].artist, songs[currentIndex].imgFilePath);
}


// Hàm xử lý sự kiện prev song
function playPrevSong() {
    currentIndex--;
    if (currentIndex < 0) {
        currentIndex = songs.length - 1; // Di chuyển đến cuối danh sách nếu đang ở đầu danh sách
    }
    playSong(songs[currentIndex].mp3FilePath, songs[currentIndex].name, songs[currentIndex].artist, songs[currentIndex].imgFilePath);
}

// Gán sự kiện cho nút next song và prev song
playNextButton.addEventListener('click', playNextSong);
playPrevButton.addEventListener('click', playPrevSong);

progressContainer.addEventListener('mousedown', function (event) {
    var rect = progressContainer.getBoundingClientRect();
    var offsetX = event.clientX - rect.left;
    var percentage = offsetX / rect.width;
    var seekTime = percentage * audioPlayer.duration;

    audioPlayer.currentTime = seekTime;
});
// Cập nhật thanh tiến trình
audioPlayer.addEventListener('timeupdate', function () {
    var percent = (audioPlayer.currentTime / audioPlayer.duration) * 100;
    progressBar.style.width = percent + '%';

    var minutes = Math.floor(audioPlayer.currentTime / 60);
    var seconds = Math.floor(audioPlayer.currentTime - minutes * 60);
    currentTime.innerText = minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
});

// Cập nhật thời lượng tổng cộng của bài hát
audioPlayer.addEventListener('loadedmetadata', function () {
    var minutes = Math.floor(audioPlayer.duration / 60);
    var seconds = Math.floor(audioPlayer.duration - minutes * 60);
    duration.innerText = minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
});

// Xử lý sự kiện kéo để tua
progressBar.addEventListener('mousedown', function (event) {
    const rect = progressBar.getBoundingClientRect();
    const offsetX = event.clientX - rect.left;
    const percentage = offsetX / rect.width;
    const seekTime = percentage * audioPlayer.duration;

    audioPlayer.currentTime = seekTime;

    // Ngăn sự kiện mặc định của thanh kéo trình duyệt
    event.preventDefault();

    // Xử lý sự kiện mousemove để tua
    document.addEventListener('mousemove', mousemoveHandler);

    // Xử lý sự kiện mouseup để kết thúc sự kiện kéo tua
    document.addEventListener('mouseup', mouseupHandler);
});

// Hàm xử lý sự kiện mousemove để tua
function mousemoveHandler(event) {
    const rect = progressBar.getBoundingClientRect();
    const offsetX = event.clientX - rect.left;
    const percentage = offsetX / rect.width;
    const seekTime = percentage * audioPlayer.duration;

    audioPlayer.currentTime = seekTime;

    // Ngăn sự kiện mặc định của thanh kéo trình duyệt
    event.preventDefault();
}

// Hàm xử lý sự kiện mouseup để kết thúc sự kiện kéo tua
function mouseupHandler(event) {
    // Hủy bỏ sự kiện mousemove và mouseup
    document.removeEventListener('mousemove', mousemoveHandler);
    document.removeEventListener('mouseup', mouseupHandler);
}

// Cập nhật thanh tiến trình khi tua
audioPlayer.addEventListener('timeupdate', updateProgress);

// Hàm cập nhật thanh progress khi tua
function updateProgress() {
    var percent = (audioPlayer.currentTime / audioPlayer.duration) * 100;
    progressBar.style.width = percent + '%';

    var minutes = Math.floor(audioPlayer.currentTime / 60);
    var seconds = Math.floor(audioPlayer.currentTime - minutes * 60);
    currentTime.innerText = minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
}
// Lấy thanh trượt âm lượng và audio player
var volumeSlider = document.getElementById('volumeSlider');

// Xử lý sự kiện khi giá trị của thanh trượt âm lượng thay đổi
volumeSlider.addEventListener('input', function () {
    audioPlayer.volume = volumeSlider.value;
});

// Cập nhật giá trị thanh trượt âm lượng dựa trên âm lượng hiện tại của audio player
audioPlayer.addEventListener('volumechange', function () {
    volumeSlider.value = audioPlayer.volume;
});
audioPlayer.addEventListener('timeupdate', function () {
    var percent = (audioPlayer.currentTime / audioPlayer.duration) * 100;
    progressBar.style.width = percent + '%';

    var minutes = Math.floor(audioPlayer.currentTime / 60);
    var seconds = Math.floor(audioPlayer.currentTime - minutes * 60);
    var currentTimeDisplay = document.getElementById('currentTime');
    currentTimeDisplay.innerText = minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
});

// Hàm lặp bài hát
loopButton.addEventListener('click', function () {
    audioPlayer.loop = !audioPlayer.loop; // Toggle the loop property

    if (audioPlayer.loop) {
        loopButton.classList.add('loop-on');
        loopButton.classList.remove('loop-off');
    } else {
        loopButton.classList.add('loop-off');
        loopButton.classList.remove('loop-on');
    }
});

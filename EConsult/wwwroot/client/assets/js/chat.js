const ROOM_ID = '@ViewBag.roomId'
let userId = null
let localStream = null
let screenStream = null;
let isScreenSharing = false;
let isMuted = false;
let videoPause = false;
const Peers = {}

const chatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/conference-hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

const myPeer = new Peer()

myPeer.on('open', id => {
    userId = id;
    const startSignalR = async () => {
        await chatConnection.start();
        await chatConnection.invoke("JoinRoom", ROOM_ID, userId);
        chatConnection.on('user-connected', id => {
            console.log(`User connected: ${id}`);
        });
    };

    startSignalR();
})

const videoGrid = document.querySelector('[video-grid]')
const myVideo = document.createElement('video')
myVideo.muted = true;

navigator.mediaDevices.getUserMedia({
    audio: true,
    video: true
}).then(stream => {
    addVideoStream(myVideo, stream)

    localStream = stream
})

chatConnection.on('user-connected', id => {
    if (userId === id) return;
    console.log("User connected: " + id)
    connectNewUser(id, localStream)
})

chatConnection.on('user-disconnected', id => {
    console.log("User disconnected: " + id)

    if (Peers[id]) Peers[id].close();
})

myPeer.on('call', call => {
    call.answer(localStream)

    const userVideo = document.createElement('video')

    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream)
    })
})

const addVideoStream = (video, stream) => {
    video.srcObject = stream;
    video.addEventListener('loadedmetadata', () => {
        video.play();
    });
    videoGrid.appendChild(video);
};

const connectNewUser = (userId, localStream) => {
    const userVideo = document.createElement('video')
    const call = myPeer.call(userId, localStream)

    call.on('stream', userVideoStream => {
        addVideoStream(userVideo, userVideoStream)
    })

    call.on('close', () => {
        userVideo.remove()
    })

    Peers[userId] = call
}

const screenShareButton = document.querySelector('#screen-share-button');
screenShareButton.addEventListener('click', async () => {
    try {
        if (!isScreenSharing) {
            screenStream = await navigator.mediaDevices.getDisplayMedia({ video: true });

            addVideoStream(myVideo, screenStream);

            Object.values(Peers).forEach(peer => {
                const sender = peer.peerConnection.getSenders().find(sender => sender.track.kind === 'video');
                sender.replaceTrack(screenStream.getVideoTracks()[0]);
            });

            isScreenSharing = true;
            screenShareButton.classList.remove('fa-desktop');
            screenShareButton.classList.add('fa-stop');
        } else {
            screenStream.getTracks().forEach(track => track.stop());

            myVideo.srcObject = localStream;

            Object.values(Peers).forEach(peer => {
                const sender = peer.peerConnection.getSenders().find(sender => sender.track.kind === 'video');
                sender.replaceTrack(localStream.getVideoTracks()[0]);
            });

            isScreenSharing = false;
            screenShareButton.classList.remove('fa-stop');
            screenShareButton.classList.add('fa-desktop');
        }
    } catch (error) {
        console.error('Error when screen sharing:', error);
    }
});

const muteButton = document.querySelector('#mute-button');
muteButton.addEventListener('click', () => {
    if (!localStream) return;

    const audioTracks = localStream.getAudioTracks();
    if (audioTracks.length === 0) return;

    audioTracks.forEach(track => {
        track.enabled = isMuted;
    });

    isMuted = !isMuted;

    if (isMuted) {
        muteButton.classList.remove('fa-volume-up');
        muteButton.classList.add('fa-microphone-slash');
    } else {
        muteButton.classList.remove('fa-microphone-slash');
        muteButton.classList.add('fa-volume-up');
    }
});

const pauseVideoButton = document.querySelector('#pause-video-button');

pauseVideoButton.addEventListener('click', function () {
    const videoTracks = localStream.getVideoTracks();
    if (videoTracks.length === 0) return;

    videoTracks.forEach(track => {
        track.enabled = videoPause;
    });

    videoPause = !videoPause;

    if (videoPause) {
        pauseVideoButton.classList.remove('fa-camera');
        pauseVideoButton.classList.add('fa-play');
    } else {
        pauseVideoButton.classList.remove('fa-play');
        pauseVideoButton.classList.add('fa-camera');
    }
});

document.querySelector('.button-container').appendChild(pauseVideoButton);

document.querySelector("#send-button").addEventListener("click", async () => {
    const messageInput = document.querySelector("#message-input");
    const message = messageInput.value;
    if (message.trim() !== "") {
        await chatConnection.invoke("SendMessage", message);
        messageInput.value = "";
    }
});

chatConnection.on("ReceiveMessage", (senderName, message) => {
    const messagesList = document.querySelector("#messages");
    const li = document.createElement("li");
    li.textContent = `${senderName}: ${message}`;
    messagesList.appendChild(li);
});

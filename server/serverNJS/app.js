const express = require('express');

const app = express();
app.use(express.json());

function makeRandomString(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() *
            charactersLength));
    }
    return result;
}

app.get(`/nextPlane/`, (req, res) => {
    try {
        let newPlane = {
            Flight: makeRandomString(5),
            Status: 'Landing',
            Color: Math.floor(Math.random() * 8) + 1
        }
        res.send(newPlane);
    }
    catch (error) {
        res.send(error)
    }

});

app.listen(3000, () => {
    setInterval(() => {
        console.log(`app is up -3000`);
    }, 2000);
});
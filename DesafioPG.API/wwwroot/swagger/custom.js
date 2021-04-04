(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');;
    document.head.removeChild(link);
    link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    document.head.removeChild(link);
    link = document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = '../img/icone.png';
    document.getElementsByTagName('head')[0].appendChild(link);
    document.title = "DesafioPG";

})();

document.addEventListener('DOMContentLoaded', () => {
    setTimeout(function () {
        if (document.readyState === "complete") {
            document.querySelector("span").innerText = "Selecione a versão";
        }
    }, 200);
});
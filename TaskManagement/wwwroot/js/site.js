// في ملف site.js أو custom.js
function confirmLogout(logoutUrl) {
    if (confirm("هل أنت متأكد من أنك تريد تسجيل الخروج؟")) {
        window.location.href = logoutUrl;
    }
}

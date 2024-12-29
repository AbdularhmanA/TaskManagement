// تعريف دالة togglePassword التي تسمح بتغيير نوع حقل كلمة المرور لرؤية النص
function togglePassword(fieldId) {
    // الحصول على عنصر حقل كلمة المرور باستخدام معرفه
    var passwordField = document.getElementById(fieldId);

    // التحقق من نوع حقل كلمة المرور وتغييره بين "password" و "text"
    if (passwordField.type === "password") {
        passwordField.type = "text"; // إذا كان نوع الحقل "password"، يتم تغييره إلى "text" لرؤية النص
    } else {
        passwordField.type = "password"; // إذا كان نوع الحقل "text"، يتم تغييره إلى "password" لإخفاء النص
    }
}

const myName=document.querySelector(".name");
console.log(myName);
console.log("THE NAME OF YOUR DESCRIPTION VARIABLE");
const darkToggle=document.getElementById("darktoggle")


if(darkToggle){
    darkToggle.addEventListener("click", function(){
        document.body.classList.toggle("dark-mode"); 

        if(document.body.classList.contains("dark-mode")){
            localStorage.setItem("darkmode", "enabled");
            darkToggle.textContent="Light Mode";
        }
        else{
            localStorage.setItem("darkmode", "disabled");
            darkToggle.textContent="Dark Mode";
        }
    })
}

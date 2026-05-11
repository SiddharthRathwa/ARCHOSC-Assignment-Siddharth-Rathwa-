const myName=document.querySelector(".name");
console.log(myName);
console.log("THE NAME OF YOUR DESCRIPTION VARIABLE");
const darkToggle=document.getElementById("darktoggle")
if(darkToggle){
    darkToggle.addEventListener("click", function(){
        document.body.classList.toggle("dark-mode"); 

        if(document.body.classList.contains("dark-mode")){
            darkToggle.textContent="Light Mode";
        }
        else{
            darkToggle.textContent="Dark Mode";
        }
    })
}
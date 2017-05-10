// Write your Javascript code.
var appLinks = {
    "Start":"<div class=\"links\"><a href=\"#\" onclick=\"drop('TASK_ID');\">Drop</a>&nbsp;|&nbsp;<a href=\"#\" onclick=\"finish('TASK_ID');\">Finish</a></div>",
    "Drop":"<div class=\"links\"><a href=\"#\" onclick=\"start('TASK_ID');\">Start</a></div>",
    "Finish":"<div class=\"links\"><a href=\"#\" onclick=\"retake('TASK_ID');\">Retake</a></div>",
    "Retake":"<div class=\"links\"><a href=\"#\" onclick=\"drop('TASK_ID');\">Drop</a>&nbsp;|&nbsp;<a href=\"#\" onclick=\"start('TASK_ID');\">Finish</a></div>",
};
function move(id,action,list){
    $.post("/Tasks/"+action+"/"+id)
    .done( task => {
        let li = $("#"+task.id);
        li.find(".links").replaceWith(appLinks[action].replace(/TASK_ID/g,id));
        list.append(li);
    } 
    );
}
function start(id){
    move(id,"Start",$("#InProgressList"));
}
function drop(id){
    move(id,"Drop",$("#ToDoList"));
}
function finish(id){
    move(id,"Finish",$("#DoneList"));
}
function retake(id){
    move(id,"Retake",$("#InProgressList"));
}

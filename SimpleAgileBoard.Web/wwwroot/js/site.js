// Write your Javascript code.
function move(id,action,list){
    $.post("/Tasks/"+action+"/"+id)
    .done( task => {
        let li = $("#"+task.id);
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

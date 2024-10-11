import { TodoPriority } from "@/types/todoPriority"
import { TodoStatus } from "@/types/todoStatus"

export const emptyTodo = () => {
  return {
    id : "",
    title : "",
    createdAt : new Date(),
    dueDate : new Date(),
    status : TodoStatus.Pending,
    tags : [],
    priority : TodoPriority.Low
  }
}

import { TodoPriority } from "./todoPriority"
import { TodoStatus } from "./todoStatus"

export type Todo = {
  id : string,
  title : string,
  createdAt : Date,
  dueDate : Date,
  status : TodoStatus,
  tags : string[],
  priority : TodoPriority
}

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

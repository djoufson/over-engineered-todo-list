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

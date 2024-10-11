import type { Todo } from "@/types/Todo";

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL;

export const getAllTodos = async () : Promise<Todo[]> => {
  try {
    var response = await fetch(`${apiBaseUrl}/todos`);
    if(response.ok) {
      var data = await response.json();
      return data as Todo[];
    } else {
      return [];
    } 
  } catch (error) {
    console.error(error);
    return [];
  }
};

export const updateTodo = async (todo:Todo): Promise<boolean> => {
  const url = `${apiBaseUrl}/todos/${todo.id}`;
  const response = await fetch(url, {
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(todo)
  });

  return response.ok;
}

export const createTodo = async (todo:Todo): Promise<Todo> => {
  const url = `${apiBaseUrl}/todos`;
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(todo)
  });
  return await response.json();
}

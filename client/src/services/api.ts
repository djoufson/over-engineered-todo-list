import { PagedList } from "@/types/pagedList";
import type { Todo } from "@/types/Todo";

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL;

export const getAllTodos = async (page:number = 1, size:number = 5) : Promise<PagedList<Todo>> => {
  const url = `${apiBaseUrl}/todos?page=${page}&size=${size}`;
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Failed to fetch data');
  }

  const data = await response.json();
  const pagedList = new PagedList<Todo>(
    data.page,
    data.size,
    data.totalCount,
    data.items.map((item: Todo) => ({
      ...item
    }))
  );

  return pagedList;
}

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

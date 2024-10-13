import { PagedList } from "@/types/pagedList";
import type { Tag } from "@/types/tag";
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
    method: 'PUT',
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

export const deleteTodo = async (id:string): Promise<boolean> => {
  const url = `${apiBaseUrl}/todos/${id}`;
  const response = await fetch(url, {
    method: 'DELETE'
  });

  return response.ok;
}

export const getTags = async (tagFilter?:string, excludedTags?:string[]): Promise<Tag[]> => {
  const url = `${apiBaseUrl}/tags?tagFilter=${tagFilter}&excludedTags=${excludedTags?.join("&excludedTags=")}`;
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Failed to fetch data');
  }

  const data = await response.json() as Tag[];
  return data ? data : [];
}

export const unassignTag = async (todoId:string, tag:string): Promise<boolean> => {
  const url = `${apiBaseUrl}/todos/${todoId}/unassign`;
  const data = { tag };
  const response = await fetch(url, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });

  return response.ok;
}
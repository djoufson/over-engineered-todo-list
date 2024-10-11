export class PagedList<T> {
  page: number;
  size: number;
  totalCount: number;
  items: T[];

  constructor(page: number, size: number, totalCount: number, items: T[]) {
    this.page = page;
    this.size = size;
    this.totalCount = totalCount;
    this.items = items;
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.size);
  }

  get hasPreviousPage(): boolean {
    return this.page > 1;
  }

  get hasNextPage(): boolean {
    return this.page < this.totalPages;
  }
}

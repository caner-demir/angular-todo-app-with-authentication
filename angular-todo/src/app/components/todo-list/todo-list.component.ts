import { Component, OnInit } from '@angular/core';
import { Todo } from 'src/app/interfaces/todo';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})

export class TodoListComponent implements OnInit {
  todos: Todo[] = [];
  todoTitle: string = '';
  filter: string = 'all';

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
    this.todoService.getAll().subscribe(todos => {
      this.todos = todos;
    })
  }

  addTodo(): void {
    if(this.todoTitle.trim().length === 0){
      return;
    }

    let todo: Todo = {
      id: 0,
      title: this.todoTitle,
      isCompleted: false,
      appUserId: 'required'
    }

    this.todoService.create(todo).subscribe(() => {
      this.todos.push(todo);
      this.todoTitle = '';
    })
  }

  updateStatus(todo: Todo): void {
    this.todoService.changeStatus(todo).subscribe();
  }
  
  deleteTodo(id: number): void {    
    this.todoService.delete(id).subscribe(() => {
      this.todos = this.todos.filter(todo => todo.id !== id);
    });
  }

  todosFiltered(): Todo[] {
    if(this.filter === 'all'){
      return this.todos;
    }
    else if(this.filter === 'active'){
      return this.todos.filter(todo => !todo.isCompleted);
    }
    else if(this.filter === 'completed'){
      return this.todos.filter(todo => todo.isCompleted);
    }

    return this.todos;
  }
}

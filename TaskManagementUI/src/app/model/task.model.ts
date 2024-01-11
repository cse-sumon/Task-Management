export class Task {
    public id: number;
    public title: string;
    public dueDate: Date;
    public priority: string;
    public status: string;
    public description?: string;
    public createdBy?: string;
    public createdByName?: string;
    public assignedTo?: string;
    public assignedToName?: string;
  
    // constructor(
    //   id: number,
    //   title: string,
    //   dueDate: Date,
    //   priority: string,
    //   status: string,
    //   description?: string,
    //   createdBy?: string,
    //   createdByName?: string,
    //   assignedTo?: string,
    //   assignedToName?: string
    // ) {
    //   this.id = id;
    //   this.title = title;
    //   this.dueDate = dueDate;
    //   this.priority = priority;
    //   this.status = status;
    //   this.description = description;
    //   this.createdBy = createdBy;
    //   this.createdByName = createdByName;
    //   this.assignedTo = assignedTo;
    //   this.assignedToName = assignedToName;
    // }
  }

  export class DropdownList{
    public id:string;
    public name:string;

    constructor(id: string, name: string) {
      this.id = id;
      this.name = name;
    }
  }
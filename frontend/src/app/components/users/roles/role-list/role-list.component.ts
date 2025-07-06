import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {RoleService, ToasterService, UsersService} from "../../../../services";
import {Role} from "../../../../models/role";
import {Router} from "@angular/router";

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  roles: Role[] = [];
  error: string = '';
  constructor(private roleService: RoleService, private router: Router, private toast: ToasterService,
              private userService: UsersService, private renderer: Renderer2, private el: ElementRef) { }
  ngOnInit() {
    this.loadRoles();
    this.newSectionUp();
  }

  loadRoles(){
    this.roleService.getRoles().subscribe({
      next: (roles) => {
        this.roles = roles;
      },
      error: (err) => {
        this.error = err.message;
        console.log("There was an error loading roles: " + this.error);
      }
    });
  }
  deleteRole(roleId: string, event: Event): void {
    event.preventDefault();

    if (confirm('Are you sure you want to delete this user?')) {
      this.roleService.deleteRole(roleId).subscribe({
        next: () => {
          this.toast.showSuccessToast(`Deleted role with the id: ${roleId}`, 'Deleted Role');
          this.loadRoles();
          this.roles = this.roles.filter(role => role.id !== roleId);
        },
        error: err => {
          this.toast.showErrorToast(`${err.message.toString()}`, 'Error deleting role');
        }
      });
    }
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#role-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#role-list-section');
    this.renderer.removeClass(section, 'active');
  }
}

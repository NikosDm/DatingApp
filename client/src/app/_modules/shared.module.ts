import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

/**FOR THE MOMENT IT DISPLAYS AN ERROR WHEN
 * IT IS IMPORTED ON APP MODULE
 */
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toastr-bottom-right',
    }),
  ],
  exports: [BsDropdownModule, ToastrModule],
})
export class SharedModule {}

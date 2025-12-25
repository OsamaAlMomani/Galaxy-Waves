import {
  SideNavInterface
} from '../../interfaces/side-nav.type';

export const ROUTES: SideNavInterface[] = [{
    path: '',
    title: 'Dashboard',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'appstore-add',
    submenu: [{
      path: '/pages/blank-page',
      title: 'Blank Page',
      iconType: '',
      icon: '',
      iconTheme: '',
      submenu: []
      },
    ]
  },
  {
    path: '',
    title: 'Authentication',
    iconType: 'nzIcon',
    iconTheme: 'outline',
    icon: 'lock',
    submenu: [{
        path: '/authentication/login-1',
        title: 'Log In',
        iconType: '',
        icon: '',

        iconTheme: '',
        submenu: []
      },
      {
        path: '/authentication/sign-up-1',
        title: 'Sign Up',
        iconType: '',
        icon: '',

        iconTheme: '',
        submenu: []
      },
      {
        path: '/authentication/forget-pass',
        title: 'Forget password',
        iconType: '',
        icon: '',
        iconTheme: '',
        submenu: []
      }
    ]
  }
]

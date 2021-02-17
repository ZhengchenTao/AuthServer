type PathName = 'permission' | 'testPage';

export interface Path {
  param: PathName;
  text: string;
}

export const path: Path[] = [
  { param: 'permission', text: '权限管理' },
  { param: 'testPage', text: 'Test Page' }
];

export const getPageTitles = (seg?: string): string => {
  const myPath = path.find((p): boolean => p.param === seg);
  if (myPath && path.some((p): boolean => p.param === seg)) {
    return `${myPath.text} - 管理系统`;
  } else {
    return `${seg} - 管理系统`;
  }
};

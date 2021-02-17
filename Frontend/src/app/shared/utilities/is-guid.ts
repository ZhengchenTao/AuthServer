export const isGuid = (value: string): boolean => {
  const reg = new RegExp(/^[0-9a-z]{8}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{12}$/);
  if (value) {
    if (reg.test(value.toLowerCase())) {
      return true;
    }
  }
  return false;
};

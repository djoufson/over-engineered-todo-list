export function formatDate(date: Date): string {
  const today = new Date();

  today.setHours(0, 0, 0, 0);
  const targetDate = new Date(date);
  targetDate.setHours(0, 0, 0, 0);

  const differenceInTime = targetDate.getTime() - today.getTime();
  const differenceInDays = Math.ceil(differenceInTime / (1000 * 3600 * 24));

  if (differenceInDays === 0) {
    return 'today';
  } else if (differenceInDays === 1) {
    return 'tomorrow';
  } else if (differenceInDays === 2 || differenceInDays === 3) {
    return `in ${differenceInDays} days`;
  }

  if (differenceInDays === -1) {
    return 'yesterday';
  } else if (differenceInDays === -2 || differenceInDays === -3) {
    return `${Math.abs(differenceInDays)} days ago`;
  }

  const daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  const currentWeekDay = today.getDay();
  const targetWeekDay = targetDate.getDay();

  if (differenceInDays > 0 && differenceInDays < 7 && targetWeekDay > currentWeekDay) {
    return daysOfWeek[targetWeekDay];
  }

  const day = targetDate.getDate().toString().padStart(2, '0');
  const month = targetDate.toLocaleString('default', { month: 'short' });
  return `${day} ${month}`;
}

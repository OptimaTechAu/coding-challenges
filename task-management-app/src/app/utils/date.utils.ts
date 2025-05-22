export const formatDateToIsoString = (date: Date | null): string | null => {
  if (!date) return null;
  return new Date(date).toISOString().split('T')[0];
};

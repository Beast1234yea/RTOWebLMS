SELECT 
    c."UnitCode",
    l."Title" as lesson_title,
    lm."MediaType",
    lm."FilePath"
FROM "LessonMedia" lm
JOIN "Lessons" l ON l."Id" = lm."LessonId"
JOIN "Modules" m ON m."Id" = l."ModuleId"
JOIN "Courses" c ON c."Id" = m."CourseId"
WHERE c."UnitCode" = 'TLILIC003'
LIMIT 20;

// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'film.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Film _$FilmFromJson(Map<String, dynamic> json) => Film(
      id: json['id'] as int,
      bango: json['bango'] as String,
      title: json['title'] as String,
      published_at: json['published_at'] == null
          ? null
          : DateTime.parse(json['published_at'] as String),
      cover_images: (json['cover_images'] as List<dynamic>?)
          ?.map((e) => e as String)
          .toList(),
    );

Map<String, dynamic> _$FilmToJson(Film instance) => <String, dynamic>{
      'id': instance.id,
      'bango': instance.bango,
      'title': instance.title,
      'published_at': instance.published_at?.toIso8601String(),
      'cover_images': instance.cover_images,
    };
